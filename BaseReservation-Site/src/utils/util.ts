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
    const unmaskedPhone = phone.replace(telephoneMaskRegex, '');
    return parseInt(unmaskedPhone, 10);
};

export const applyPhoneMask = (phone: string): string => {
    const numericPhone = phone.replace(/\D/g, '');
    if (numericPhone.length !== 8) {
        return phone;
    }
    return `${numericPhone.slice(0, 4)}-${numericPhone.slice(4)}`;
};