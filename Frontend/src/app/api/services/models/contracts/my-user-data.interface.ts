import { IOrganization } from "./organization.interface";
import { IRole } from "./role.interface";

export interface IMyUserData {
    username?: string;
    displayName: string;
    roles?: IRole[];
    organizationId?: string;
    organization?: IOrganization;
}