import { ITitleData } from "../../base/contracts/title-data.interface";

export interface IArticle extends ITitleData {
    summary: string;
    description: string;
    link: string;
    picture: string;
    newsletterId: string;
    newsletter: any;
    createdAt: Date;
    createdById: string;
    createdBy: any;
    published: boolean;
    publishedAt?: Date;
    publishedById?: string;
    publishedBy: any;
}