import { Routes } from '@angular/router';
import { QuestionDetails } from './question-details/question-details';
import { Question } from './models/question';       
import { Home } from './home/home';
export const routes: Routes = [
   { path: '', component: Home }, // Landing on localhost:4200 shows Home
  { path: 'question/:id', component: QuestionDetails },
    { path: '**', redirectTo: '' } 
];
