pattern = /(.*?)a(?!(a+)b\2c)\2(.*)/;
string = 'baaabaac';
actualmatch = string.match(pattern);
expectedmatch = Array('baaabaac', 'ba', undefined, 'abaac');
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

pattern = /(aa|aabaac|ba|b|c)*/;
string = 'aabaac';
actualmatch = string.match(pattern);
expectedmatch = Array('aaba', 'ba');
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

var r = /\w+@\w+\.com/;
var r = new RegExp('\\w+@\\w+\\.com');

pattern = /a|ab/;
string = 'abc';
actualmatch = string.match(pattern);
expectedmatch = Array('a');
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

pattern = /((a)|(ab))((c)|(bc))/;
string = 'abc';
actualmatch = string.match(pattern);
expectedmatch = Array('abc', 'a', 'a', undefined, 'bc', undefined, 'bc');
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

pattern = /a[a-z]{2,4}/;
string = 'abcdefghi';
actualmatch = string.match(pattern);
expectedmatch = Array('abcde');
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

pattern = /a[a-z]{2,4}?/;
string = 'abcdefghi';
actualmatch = string.match(pattern);
expectedmatch = Array('abc');
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

pattern = /^(a+)\1*,\1+$/;
string = 'aaaaaaaaaa,aaaaaaaaaaaaaaa';
actualmatch = string.match(pattern);
expectedmatch = Array('aaaaaaaaaa,aaaaaaaaaaaaaaa', 'aaaaa');
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

pattern = /(z)((a+)?(b+)?(c))*/;
string = 'zaacbbbcac';
actualmatch = string.match(pattern);
expectedmatch = Array('zaacbbbcac', 'z', 'ac', 'a', 'bbb', 'c');
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

pattern = /(a*)*/;
string = 'b';
actualmatch = string.match(pattern);
expectedmatch = Array('', undefined);
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

pattern = /(a*)b\1+/;
string = 'baaaac';
actualmatch = string.match(pattern);
expectedmatch = Array('b', '');
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

pattern = /(?=(a+))/;
string = 'baaabac';
actualmatch = string.match(pattern);
expectedmatch = Array('', 'aaa');
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

pattern = /(?=(a+))a*b\1/;
string = 'baaabac';
actualmatch = string.match(pattern);
expectedmatch = Array('aba', 'a');
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

pattern = /(?=(a+))/;
string = 'baaabac';
actualmatch = string.match(pattern);
expectedmatch = Array('', 'aaa');
assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());

assert('/x/g', /x/g.toString());

pattern = /(\S+)/g;
string = ' -0.1344972E+01 -0.4862373E+01 -0.1942746E+01';
actualmatch = string.match(pattern);
expectedmatch = Array('-0.1344972E+01', '-0.4862373E+01', '-0.1942746E+01');
//assert(expectedmatch.length, actualmatch.length);
assert(expectedmatch.toString(), actualmatch.toString());
