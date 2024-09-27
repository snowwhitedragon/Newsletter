import { ITitleData } from "../../base/contracts/title-data.interface";

export interface IOrganization extends ITitleData {
    responsibilityType: number;
    description: string;
}