import {LookupMember} from "../lookup/lookupMember.model";
import {WallPostReplyModel} from "./wallPostReply.model";

export interface WallPostModel {
  id: number;
  content: string;
  createdAt: Date;
  author: LookupMember;
  replies: WallPostReplyModel[];
}
