import { LimitStringLength } from "./limit-string-length";

describe('LimitStringLength', () => {
  it('create an instance', () => {
    const pipe = new LimitStringLength();
    expect(pipe).toBeTruthy();
  });
});
