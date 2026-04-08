import { Answer } from './answer';
export interface Question {
    id: string;
    title: string;
    content: string;
    authorName: string;
    isApproved: boolean;
    createdAt: string;
    answers: Answer[];
}
