import {LookupMember} from "../lookup/lookup-member.model";
import {WallPostReplyModel} from "./wallPostReply.model";

export interface WallPostModel {
  id: number;
  content: string;
  createdAt: Date;
  author: LookupMember;
  replies: WallPostReplyModel[];
}
