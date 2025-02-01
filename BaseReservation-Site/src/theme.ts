import { createTheme } from '@mui/material/styles';
import paintedWallBackground from 'assets/painted-wall-background.webp';;

export const theme = createTheme({
    typography: {
        fontFamily: `'Satoshi', sans-serif`,
        h1: {
            fontFamily: `'Angel Rhapsody', serif`,
            fontSize: '7rem', // Equivalent to --h1-font-size
        },
        h2: {
            fontFamily: `'Angel Rhapsody', serif`,
            fontSize: '4rem', // Equivalent to --h2-font-size
        },
        h3: {
            fontFamily: `'Angel Rhapsody', serif`,
            fontSize: '2rem', // Equivalent to --h3-font-size
        },
        h4: {
            fontFamily: `'Angel Rhapsody', serif`,
            fontSize: '1.5rem', // Equivalent to --h4-font-size
        },
        body1: {
            fontFamily: `'Satoshi', sans-serif`,
            fontSize: '1.25rem', // Equivalent to --normal-font-size
        },
        body2: {
            fontFamily: `'Satoshi', sans-serif`,
            fontSize: '1.125rem', // Equivalent to --small-font-size
            fontWeight: 'bold'
        },
        subtitle1: {
            fontFamily: `'Satoshi', sans-serif`,
            fontSize: '1.25rem', // Equivalent to --h3-font-size
        },
        subtitle2: {
            fontFamily: `'Satoshi', sans-serif`,
            fontSize: '1rem',
            color: 'grey'
        },
        fontWeightMedium: 500, // Equivalent to --font-medium
        fontWeightRegular: 600, // Equivalent to --font-semi-bold
    },
    palette: {
        common: {
            white: '#fff',
        },
        primary: {
            main: '#2A2722',
        },
        secondary: {
            main: '#ff4081',
        },
        background: {
            default: '#1E1C17',
            paper: '#FFF4EB',
        },
        text: {
            primary: '#FFF4EB',
            secondary: '#B77B56'
        }
    },
    components: {
        MuiCssBaseline: {
            styleOverrides: {
                body: {
                    backgroundImage: `url(${paintedWallBackground})`,
                    backgroundSize: 'cover',
                    backgroundPosition: 'center',
                    backgroundColor: 'transparent',
                    color: '#fff',
                },
            },
        },
        MuiMenu: {
            styleOverrides: {
                paper: {
                    backgroundColor: '#2A2722',
                }
            }
        },
        MuiMenuItem: {
            styleOverrides: {
                root: {
                    '&:hover': {
                        backgroundColor: '#3D3B36',
                    },
                },
            },
        },
        MuiDrawer: {
            styleOverrides: {
                paper: {
                    width: '70%',
                    maxWidth: '70%',
                    backgroundColor: '#2A2722',
                    paddingTop: '2rem',
                    paddingLeft: '1rem'
                },
            },
        },
        MuiButton: {
            styleOverrides: {
                containedPrimary: {
                    backgroundColor: '#DB9F6A',
                    height: '3rem',
                    color: '#fff',
                    fontWeight: 'bold',
                    '&:hover': {
                        backgroundColor: '#B77B56'
                    }
                },
                outlinedPrimary: {
                    height: '3rem',
                    color: '#fff',
                    borderColor: 'white',
                    fontWeight: 'bold',
                    '&:hover': {
                        backgroundColor: '#3D3B36'
                    }
                }
            },
        },
        MuiLink: {
            styleOverrides: {
                root: {
                    color: 'white',
                    textDecoration: 'none',
                    '&:hover': {
                        color: '#ff4081',
                        textDecoration: 'underline',
                    },
                },
            },
        },
        MuiOutlinedInput: {
            styleOverrides: {
                root: {
                    '& .MuiOutlinedInput-notchedOutline': {
                        borderColor: 'white',
                    },
                    '&:hover .MuiOutlinedInput-notchedOutline': {
                        borderColor: '#B77B56',
                    },
                    '&.Mui-focused .MuiOutlinedInput-notchedOutline': {
                        borderColor: '#DB9F6A',
                    },
                },
                input: {
                    fontSize: '1rem', // Adjust the font size as needed
                },
                inputSizeSmall: {
                    fontSize: '0.75rem', // Adjust the font size for small input as needed
                },
            },
        },
        MuiFormLabel: {
            styleOverrides: {
                root: {
                    fontSize: '1.25rem', // Adjust the font size as needed
                    '&.Mui-focused': {
                        color: '#DB9F6A',
                        fontSize: '1.25rem',
                    },
                },
            },
        },
        MuiFormControlLabel: {
            styleOverrides: {
                label: {
                    color: 'inherit',
                    '&.Mui-disabled': {
                        color: 'inherit',
                    },
                },
            },
        },
        MuiSwitch: {
            styleOverrides: {
                switchBase: {
                    color: 'grey',
                    '&.Mui-checked': {
                        color: '#B77B56',
                    },
                    '&.Mui-checked + .MuiSwitch-track': {
                        backgroundColor: '#B77B56',
                    },
                },
                track: {
                    backgroundColor: 'lightGrey',
                },
            },
        },
        MuiSelect: {
            styleOverrides: {
                icon: {
                    color: 'white',
                },
            },
        },
        MuiDivider: {
            styleOverrides: {
                root: {
                    borderColor: "white"
                }
            }
        }
    },
});
