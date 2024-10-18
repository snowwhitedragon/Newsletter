import { ITitleData } from "../../base/contracts/title-data.interface";
import { INewsletter } from "./newsletter.interface";

export interface IOrganization extends ITitleData {
    description: string;
    newsletters: INewsletter[];
}