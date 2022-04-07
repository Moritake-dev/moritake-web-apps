import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RouterModule } from '@angular/router';
import { NgxSpinnerModule,NgxSpinnerService } from 'ngx-spinner';


import { NgxPaginationModule } from 'ngx-pagination';
import { NgaReadMoreModule } from "nga-read-more";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ModalModule } from 'ngx-bootstrap';



import { SharedModule } from '../shared/shared.module';
import { InformationBoardComponent } from './information-board/information-board.component';
import { InformationBoardRoutingModule } from './information-board-routing.module';
import { InformationBoardCreateComponent } from './information-board-create/information-board-create.component';
import { InformationBoardShowComponent } from './information-board-show/information-board-show.component';
import { InformationBoardEditComponent } from './information-board-edit/information-board-edit.component';
import { TopBarComponent } from '../top-bar/top-bar.component';
import { TranslateModule } from '@ngx-translate/core';




@NgModule({
  declarations: [
    TopBarComponent,
    InformationBoardComponent,
    InformationBoardCreateComponent,
    InformationBoardShowComponent,
    InformationBoardEditComponent,
    
    
  ],
  imports: [

    CommonModule,
    RouterModule,
    InformationBoardRoutingModule,
    TranslateModule,
    NgxPaginationModule,
    NgaReadMoreModule,
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    CKEditorModule,
    ModalModule.forRoot(),
    SharedModule 

  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  exports: [InformationBoardCreateComponent, TopBarComponent],
  providers: [NgxSpinnerService],
})
export class InformationBoardModule { }
