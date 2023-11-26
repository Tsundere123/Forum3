import {LookupMember} from "./lookupMember.model";

export interface LookupThread {
  id: number;
  title: string;
  createdAt: string;
  category: string;
  creator: LookupMember;
}
