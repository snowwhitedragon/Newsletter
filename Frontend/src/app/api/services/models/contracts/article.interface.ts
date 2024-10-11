import { ITitleData } from "../../base/contracts/title-data.interface";
import { IMyUserData } from "./my-user-data.interface";
import { INewsletter } from "./newsletter.interface";

export interface IArticle extends ITitleData {
    summary: string;
    description: string;
    link?: string;
    picture?: string;
    newsletterId: string;
    newsletter: INewsletter;
    createdAt?: Date;
    createdById?: string;
    createdBy?: IMyUserData;
    published: boolean;
    publishedAt?: Date;
    publishedById?: string;
    publishedBy?: IMyUserData;
}