import { Pipe, PipeTransform } from '@angular/core';
import * as moment from "moment";

@Pipe({ name: 'convertToReadableDate' })

export class ConvertToReadableDate implements PipeTransform {
  transform(value: string, format: string): string {
    return moment(value).format(format);
  }
}
