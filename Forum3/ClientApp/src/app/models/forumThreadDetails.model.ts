export interface ForumThreadDetailsModel{
  id: number;
  title: string;
  creator: string;
  isPinned: boolean;
  isSoftDeleted: boolean;
  isLocked: boolean;
}
