export class Response {

    public isOk!: boolean;
    public typeOfError: ErrorTypes | undefined;
    private error: any;

    private constructor(isOk: boolean, error: any) {
        this.isOk = isOk;
        if (error != null && isOk != true) {
            this.error = error;
            this.typeOfError = this.getTypeOfError(error.errorType);
        }
    }

    public static fromPromise(promise: Promise<any>, onSuccess: (response: any) => void): Promise<Response> {
        return new Promise<Response>((resolve, _) => {
            promise.then(response => {
                onSuccess(response);
                resolve(new Response(true, null));
            }, error => {
                    resolve(new Response(false, error.response.data));
            });
        });
    }

    public process(onAny?: IErrorFunction<any>, onValidationError?: IErrorFunction<IValidationDetails>): void {
        if (this.isOk != true && onAny != null)
            onAny({
                errorCode: this.error.errorCode,
                details: this.error.details
            });
        if (this.typeOfError == ErrorTypes.validation && onValidationError != null)
            onValidationError({
                errorCode: this.error.errorCode,
                details: this.error.details
            });
        // add here...
    }

    private getTypeOfError(error: string): ErrorTypes | undefined {
        let errorStr = new Map([
            ["validation", ErrorTypes.validation],
            ["exception", ErrorTypes.exception],
            ["notFound", ErrorTypes.notFound],
            ["forbidden", ErrorTypes.forbidden],
            ["notAuthorized", ErrorTypes.notAuthorized],
            ["undefined", ErrorTypes.undefined]
        ]);
        return errorStr.get(error);
    }

    
}

export enum ErrorTypes {
    validation,
    exception,
    notFound,
    forbidden,
    notAuthorized,
    undefined
}

export interface IErrorFunction<T> {
    (details: IErrorDetails<T>): void;
}

export interface IErrorDetails<T> {
    errorCode: string;
    details: T;
}

export interface IValidationDetails {
    success: boolean;
    errorCount: number;
    listOfErrors: Array<IValidationError>;
}

export interface IValidationError {
    errorMessage: string;
    propertyName: string;
}