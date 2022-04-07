import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { InformationBoardShowComponent } from './information-board-show/information-board-show.component';
import { InformationBoardCreateComponent } from './information-board-create/information-board-create.component';
import { InformationBoardComponent } from './information-board/information-board.component';
import { InformationBoardEditComponent } from './information-board-edit/information-board-edit.component';


const broadcastRoutes: Routes = [
  {
    path: "information-board",
    component: InformationBoardComponent,
    children: [
      {path: "", component: InformationBoardShowComponent },
      {path: "create", component: InformationBoardCreateComponent },
      {
        path: 'edit',
        children: [
          { path: ':id', component: InformationBoardEditComponent},
        ]
      }
    ]
    
  }
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(broadcastRoutes, { onSameUrlNavigation: 'reload' })
  ]
})
export class InformationBoardRoutingModule { }
