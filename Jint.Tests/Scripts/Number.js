assert(true, isNaN(NaN));
assert(true, isNaN("string"));
assert(false, isNaN("12"));
assert(false, isNaN(12));

assert(169, 0251);
assert(169, 0xA9);

assert('2n9c', Number(123456).toString(36));

assert(1, new Number(1));
assert('1', (new Number(1)).toString());
