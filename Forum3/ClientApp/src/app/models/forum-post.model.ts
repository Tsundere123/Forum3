import { LookupMember } from "./lookup/lookup-member.model";

export interface ForumPost{
  id: number;
  content: string;
  isSoftDeleted: boolean;
  createdAt: Date;
  creator: LookupMember;
  editedBy: LookupMember;
  editedAt: Date;
}
