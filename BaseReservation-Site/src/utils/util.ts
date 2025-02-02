/* eslint-disable @typescript-eslint/no-explicit-any */
import { ApiError } from "openapi-typescript-fetch";
import { BaseReservationErrorDetails } from "types/api-basereservation";

export const telephoneMaskRegex = /^\d{4}-\d{4}$/;

export const isPresent = <T>(t: T): t is NonNullable<T> => {
    return t !== null && t !== undefined;
};

export const convertToArray = <T>(value: T | readonly T[] | undefined): T[] => {
    if (!value) return [];

    if (Array.isArray(value)) {
        return Array.from(value);
    }

    return [value as T];
}

export const removePhoneMask = (phone: string): number => {
    const unmaskedPhone = phone.replace(/[^\d]/g, '');
    return parseInt(unmaskedPhone, 10);
};

export const applyPhoneMask = (phone: string): string => {
    const numericPhone = phone.replace(/\D/g, '');
    if (numericPhone.length !== 8) {
        return phone;
    }
    return `${numericPhone.slice(0, 4)}-${numericPhone.slice(4)}`;
};

const toCamelCase = (str: string): string => {
    return str.replace(/([A-Z])/g, (match) => `_${match.toLowerCase()}`).replace(/^_/, "");
};

export const transformErrorKeys = (error: Record<string, any>): Record<string, any> => {
    const transformedError: Record<string, any> = {};
    for (const key in error) {
        if (Object.prototype.hasOwnProperty.call(error, key)) {
            transformedError[toCamelCase(key)] = error[key];
        }
    }
    return transformedError;
};

export const getErrorMessage = (error: ApiError) => {
    const errorDetail = transformErrorKeys(error.data as BaseReservationErrorDetails);
    return errorDetail.message;
}
