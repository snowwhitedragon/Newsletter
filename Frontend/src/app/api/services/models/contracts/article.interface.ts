import { ITitleData } from '../../base/contracts/title-data.interface';

export interface IArticle extends ITitleData {
  summary: string;
  description: string;
  newPicture?: string;
  previewPicture?: string;
  newsletterId?: string;
  organizationId?: string;
  updatedAt?: Date;
  updatedByName?: string;
  published?: boolean;
  publishedAt?: Date;
  publishedByName?: string;
}
