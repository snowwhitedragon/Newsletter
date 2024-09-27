import { IData } from "../../base/contracts/data.interface";

export interface IContact extends IData {
    readableId: string;
    salutation: string;
    firstName: string;
    lastName: string;
    email: string;
    stateId: string;
    state: any;
    languageId: string;
    language: any;
}