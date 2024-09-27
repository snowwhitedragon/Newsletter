import { ITitleData } from "../../base/contracts/title-data.interface";
import { IArticle } from "./article.interface";
import { IContact } from "./contact.interface";
import { IOrganization } from "./organization.interface";

export interface INewsletter extends ITitleData {
    description: string;
    articles: IArticle[];
    contacts: IContact[];
    organizatizations: IOrganization[];
}