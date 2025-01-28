/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable react-refresh/only-export-components */
import Cookies from 'js-cookie';
import { jwtDecode } from 'jwt-decode';
import { ReactNode, useCallback } from 'react';
import { useSnackbar } from 'stores/useSnackbar';
import { LoginTypeForm } from 'pages/Login/LoginSchema';
import { createContext, useState, useContext, useEffect } from 'react';
import { Authentication, BaseReservationErrorDetails } from 'types/api-basereservation';
import { usePostAuthentication } from 'hooks/api-basereservation/usePostAuthentication';
import { usePostRefreshAuthentication } from 'hooks/api-basereservation/usePostRefreshAuthentication';

interface AuthContextType {
    isAuthenticated: boolean;
    login: (data: LoginTypeForm) => Promise<void>;
    logout: () => void;
    refreshTokens: () => void;
    authLoaded: boolean;
}

interface DecodedToken {
    exp: number;
    [key: string]: unknown;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

const AuthProvider = ({ children }: { children: ReactNode }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [authLoaded, setAuthLoaded] = useState(false);
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const getToken = useCallback(() => Cookies.get('access_token'), []);
    const getRefreshToken = useCallback(() => Cookies.get('refresh_token'), []);

    const { mutate: postAuthenticationUser } = usePostAuthentication({
        onSuccess: (data: Authentication) => {
            setSnackbarMessage('Inicio de sesión válido');
            Cookies.set('access_token', String(data.token), { expires: 1 / 24 });
            Cookies.set('refresh_token', String(data.refreshToken), { expires: 30 });
            setIsAuthenticated(true);
        },
        onError: (data: BaseReservationErrorDetails) => {
            setSnackbarMessage(`${data.message}`, 'error');
            logout();
        },
    });

    const login = async (data: LoginTypeForm) => {
        try {
            postAuthenticationUser(data);
        } catch (error) {
            setSnackbarMessage(`Error al intentar iniciar sesión: ${error}`, 'error');
        }
    };

    const logout = useCallback(() => {
        Cookies.remove('access_token');
        Cookies.remove('refresh_token');
        setIsAuthenticated(false);
    }, []);

    const { mutate: refreshTokenMutation } = usePostRefreshAuthentication({
        onSuccess: (data: Authentication) => {
            Cookies.set('access_token', String(data.token), { expires: 1 / 24 });
            Cookies.set('refresh_token', String(data.refreshToken), { expires: 30 });
            setIsAuthenticated(true);
        },
        onError: (data: BaseReservationErrorDetails) => {
            setSnackbarMessage(`Error actualizando tokens: ${data.message}`, 'error');
            logout();
        },
    });

    const refreshTokens = useCallback(() => {
        const token = getToken();
        const refreshToken = getRefreshToken();

        if (!token || !refreshToken) {
            setSnackbarMessage(`Por favor inice sesión`, 'error');
            logout();
            return;
        }

        try {
            const decodedToken: DecodedToken = jwtDecode<DecodedToken>(token);
            const currentTime = Date.now() / 1000;

            if (decodedToken.exp && decodedToken.exp < currentTime) {
                setSnackbarMessage('El token ha expirado. Refrescando...', 'info');
                refreshTokenMutation({ token, refreshToken });
            }
        } catch (error) {
            setSnackbarMessage('Error al intentar verificar el token', 'error');
        }
    }, [getToken, getRefreshToken, setSnackbarMessage, refreshTokenMutation, logout]);

    useEffect(() => {
        if (isAuthenticated) {
            refreshTokens();
        }
    }, [isAuthenticated, refreshTokens]);

    useEffect(() => {
        const token = getToken();
        const refreshToken = getRefreshToken();

        if (token && refreshToken) {
            try {
                const decodedToken: DecodedToken = jwtDecode<DecodedToken>(token);
                const currentTime = Date.now() / 1000;

                if (decodedToken.exp && decodedToken.exp < currentTime) {
                    refreshTokens();
                } else {
                    setIsAuthenticated(true);
                }
            } catch (error) {
                setSnackbarMessage('Error al intentar verificar el token', 'error');
                logout();
            }
        } else {
            setIsAuthenticated(false);
        }

        setAuthLoaded(true);
    }, [getToken, getRefreshToken, logout, refreshTokens, setSnackbarMessage]);

    return (
        <AuthContext.Provider value={{ isAuthenticated, login, logout, refreshTokens, authLoaded }}>
            {children}
        </AuthContext.Provider>
    );
};

const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth debe ser usado dentro de un AuthProvider');
    }
    return context;
};


export { AuthProvider, useAuth };