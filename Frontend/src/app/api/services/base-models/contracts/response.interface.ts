export interface IResponse<T> {
    result: T;
    errors: string[];
    isSuccess: boolean;
}