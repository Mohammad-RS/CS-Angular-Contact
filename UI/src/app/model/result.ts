export interface Result<T> {
    success: Boolean;
    data: T;
    errorCode: number;
    errorMessage: string;
}
