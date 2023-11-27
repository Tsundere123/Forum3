import {LookupMember} from "../lookup/lookupMember.model";
import { WallPostModel } from "./wallPost.model";

export interface ProfileWallModel {
  user: LookupMember;
  wall: WallPostModel[];
}
