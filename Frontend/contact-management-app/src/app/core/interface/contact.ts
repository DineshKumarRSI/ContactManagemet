export interface IContactResult{
    data:IContact[],
    totalRecord:number
}

export interface IContact {
    id: number,
    firstName: string,
    lastName: string,
    email: string
}
