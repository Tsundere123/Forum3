import {LookupMember} from "../lookup/lookup-member.model";

export interface WallPostReplyModel {
  id: number;
  content: string;
  createdAt: Date;
  author: LookupMember;
}
