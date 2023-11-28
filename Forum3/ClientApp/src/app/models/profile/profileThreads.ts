import {LookupMember} from "../lookup/lookup-member.model";
import {LookupThread} from "../lookup/lookup-thread.model";

export interface ProfileThreadsModel {
  user: LookupMember;
  threads: LookupThread[];
}
