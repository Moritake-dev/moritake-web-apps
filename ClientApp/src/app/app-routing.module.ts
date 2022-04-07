import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthCallbackComponent } from './account/auth-callback/auth-callback.component';
import { InformationBoardCreateComponent } from './information-board/information-board-create/information-board-create.component';
import { InformationBoardEditComponent } from './information-board/information-board-edit/information-board-edit.component';
import { InformationBoardShowComponent } from './information-board/information-board-show/information-board-show.component';
import { LandingPageComponent } from './landing-page/landing-page.component';

const routes: Routes = [
  { path: 'auth-callback', component: AuthCallbackComponent},
  { path: 'home', component: InformationBoardShowComponent},
  { path: 'create', component: InformationBoardCreateComponent},
  { path: 'edit/:id', component: InformationBoardEditComponent},
  { path: 'login', component: LandingPageComponent },
  { path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
