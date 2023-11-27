import {LookupMember} from "../lookup/lookupMember.model";
import {LookupThread} from "../lookup/lookupThread.model";

export interface ProfileThreadsModel {
  user: LookupMember;
  threads: LookupThread[];
}
