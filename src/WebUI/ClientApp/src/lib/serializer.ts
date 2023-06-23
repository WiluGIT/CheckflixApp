type Params = {
    [key: string]: string | string[] | number | number[] | boolean | boolean[] | null | undefined;
};

export const customParamsSerializer = <T extends Params>(params: T): string => {
    const encodedParams = Object.entries(params).map(([key, value]) => {
        if (!value) {
            return '';
        }

        if (Array.isArray(value)) {
            return value.map((v) => `${encodeURIComponent(key)}=${encodeURIComponent(v)}`).join('&');
        }
        return `${encodeURIComponent(key)}=${encodeURIComponent(value)}`;
    });
    return encodedParams.join('&');
};
