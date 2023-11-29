import {LookupMember} from "./lookup-member.model";

export interface LookupPost {
  id: number;
  threadId: number;
  threadTitle: string;
  content: string;
  createdAt: Date;
  creator: LookupMember;
  isSoftDeleted: boolean;
}
