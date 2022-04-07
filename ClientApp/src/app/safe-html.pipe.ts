import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer, SafeHtml, SafeStyle, SafeScript, SafeUrl, SafeResourceUrl } from '@angular/platform-browser';

@Pipe({
  name: 'safeHtml'
})
export class SafeHtmlPipe implements PipeTransform {

  constructor(private  sanitizer: DomSanitizer) {}

 public transform(value: any): any  {
  
   return this.sanitizer.bypassSecurityTrustHtml(value);
  
  }

}
