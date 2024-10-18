import { ITitleData } from "../../base/contracts/title-data.interface";
import { IArticle } from "./article.interface";

export interface INewsletter extends ITitleData {
    description?: string;
    articles?: IArticle[];
}