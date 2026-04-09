import { Routes } from '@angular/router';
import { QuestionDetails } from './question-details/question-details';
import { Question } from './models/question';
export const routes: Routes = [
    {path: 'question/:id', component: QuestionDetails},
    { path: '**', redirectTo: '' }
];
