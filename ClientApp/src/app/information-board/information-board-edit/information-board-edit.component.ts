import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
/* import { BroadcastMessageCreateModel } from 'src/models/broadcastMessage';
import { AuthenticationService } from 'src/services/authentication.service'; */
import { ActivatedRoute, Params, Router } from '@angular/router';
import { BsModalRef , BsModalService} from 'ngx-bootstrap';
import { CommonService } from '../../../services/common.service';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { switchMap } from 'rxjs/operators';
import { MessageEditModel } from 'src/models/message';


@Component({
  selector: 'app-information-board-edit',
  templateUrl: './information-board-edit.component.html',
  styleUrls: ['./information-board-edit.component.css']
})
export class InformationBoardEditComponent implements OnInit {

  public Editor = ClassicEditor;

  messageEditForm: FormGroup;
  submitted = false;
  
  messageFormData: any = {}; 

  message;
  messageTitle = '';
  messageDetail = '';


  // ngx-modal configuration and implementation
  modalRef: BsModalRef;
  modalConfig = {
    backdrop: true,
    class: "modal-xl"
  };
  constructor(
    private formBuilder: FormBuilder,
    /* private authService: AuthenticationService,
    private broadcastService: BroadcastMessageServiceService, */
    private actRoute: ActivatedRoute,
    private router: Router,
    private modalService: BsModalService,
    private commonService: CommonService,
  ) {
    

      ClassicEditor.defaultConfig = {
        toolbar: {
          items: [
            'heading',
            '|',
            'bold',
            'italic',
            '|',
            'bulletedList',
            '|',
            'undo',
            'redo',
            'fontColor',
            'underline'
          ]
        },
      };

      this.actRoute.params
      .pipe(
        switchMap((params: Params) =>
          this.commonService.getInfoById(+params["id"])
        )
      )
      .subscribe(data => {
        this.message = data;
        if (this.message.messageTitle !== null) {
          this.messageTitle = this.message.messageTitle;
        }
        if (this.message.messageDetail !== null) {
          this.messageDetail = this.message.messageDetail;
        }
        this.messageEditFormControl.messageTitle.setValue(this.messageTitle);
        this.messageEditFormControl.messageBody.setValue(this.messageDetail);
        
        
        console.log(data);
      });

   }

  ngOnInit() {
    this.messageEditForm = this.formBuilder.group({
      messageTitle: ["", Validators.required],
      messageBody: ["", Validators.required],
    });
  }

  get messageEditFormControl() {
    return this.messageEditForm.controls;
  }

   onSubmit(template: TemplateRef<any>) {
    this.submitted = true;
    if (this.messageEditForm.invalid) {
      return;
    }
    this.messageFormData.messageTitle = this.messageEditForm.controls["messageTitle"].value;
    this.messageFormData.messageBody = this.messageEditForm.controls["messageBody"].value;



    this.modalRef = this.modalService.show(template, this.modalConfig);
    console.log(this.messageFormData);
    
  } 

  modalConfirm() {

    let messageModel = new MessageEditModel();

    messageModel.id = this.message.id;
    messageModel.messageTitle = this.messageFormData.messageTitle;
    messageModel.messageDetail = this.messageFormData.messageBody;
    
    this.commonService.updateMessage(messageModel, messageModel.id).subscribe(
      data => {
        console.log(data);
        this.router.navigateByUrl("/", { skipLocationChange: true }).then(() => {
          this.router.navigate(["/home"]);
        });
      },
      error => {
        console.log(error);
      }
    );
    
    this.modalRef.hide();

  }

  modalDecline() {
    this.submitted = false;
    if (this.messageEditForm.invalid) {
      return;
    }

   this.modalRef.hide();
  }

}
