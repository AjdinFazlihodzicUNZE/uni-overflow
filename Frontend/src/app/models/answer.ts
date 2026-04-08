import { Question } from "./question";

export interface Answer {
    id: string;
    content: string;
    authorName: string;
    isApproved: boolean;
    createdAt: string;
    questionId: string;
    question?: Question;
}
