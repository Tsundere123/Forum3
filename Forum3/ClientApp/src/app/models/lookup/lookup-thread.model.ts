import { LookupMember } from "./lookup-member.model";

export interface LookupThread {
  id: number;
  title: string;
  createdAt: Date;
  category: string;
  creator: LookupMember;
}
