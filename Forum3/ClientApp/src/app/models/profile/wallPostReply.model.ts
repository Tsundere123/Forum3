import {LookupMember} from "../lookup/lookupMember.model";

export interface WallPostReplyModel {
  id: number;
  content: string;
  createdAt: Date;
  author: LookupMember;
}
