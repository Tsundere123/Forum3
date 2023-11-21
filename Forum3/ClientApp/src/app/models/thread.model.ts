export interface Thread{
  id: number;
  title: string;
  categoryId: number;
  creatorId: string;
  createdAt: Date;
  editedAt: Date;
  editedBy: string;
  isSoftDeleted: boolean;
  isPinned: boolean;
  isLocked: boolean;
}
