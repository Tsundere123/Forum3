import {LookupMember} from "./lookupMember.model";

export interface LookupPost {
  id: number;
  threadId: number;
  threadTitle: string;
  content: string;
  createdAt: Date;
  creator: LookupMember;
}
