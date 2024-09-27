import { ICodeTitleData } from "../../base/contracts/code-title-data.interface";

export interface IRole extends ICodeTitleData {
    id: string;
    code: string;
    title: string;
}