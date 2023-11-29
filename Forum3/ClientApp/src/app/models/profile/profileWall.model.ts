import {LookupMember} from "../lookup/lookup-member.model";
import { WallPostModel } from "./wallPost.model";

export interface ProfileWallModel {
  user: LookupMember;
  wall: WallPostModel[];
}
