var myVar = 42;
var myArray = new Array('Hello', myVar, 3.14159);

assert('Hello', myArray[0]);
assert(42, myArray[1]);
assert(3.14159, myArray[2]);
assert(3, myArray.length);
assert(undefined, myArray[3]);
assert(3, myArray.length);
myArray.push(undefined);
assert(4, myArray.length);

var myVar = 42;
var myArray = ['Hello', myVar, 3.14159];

assert('Hello', myArray[0]);
assert(42, myArray[1]);
assert(3.14159, myArray[2]);
assert(undefined, myArray[3]);
assert(3, myArray.length);
myArray.push(undefined);
assert(4, myArray.length);

myArray = [1, 2, 3, 4, 5];
assert(2, myArray.slice(0, 2).length);
assert(3, myArray.slice(0, -2).length);
assert(1, myArray.slice(2, -2).length);

assert(5, myArray.length);
assert(1, myArray.shift());
assert(4, myArray.length);
assert(2, myArray.shift());
assert(3, myArray.length);


myArray.reverse();
assert(3, myArray.length);
assert(5, myArray[0]);
assert(4, myArray[1]);
assert(3, myArray[2]);
myArray.reverse();
assert(3, myArray.length);
assert(3, myArray[0]);
assert(4, myArray[1]);
assert(5, myArray[2]);

var myArray2 = [1, 2, 3];
var myArray3 = myArray2.concat(myArray);
assert(6, myArray3.length);
assert(3, myArray3[2]);
assert(3, myArray3[3]);

assert("1,2,3,3,4,5", myArray3.join());
assert("1;2;3;3;4;5", myArray3.join(';'));

var myArray4 = myArray3.splice(3, 1);
assert(5, myArray3.length);
assert(1, myArray4.length);

assert(8, myArray3.unshift(6, 7, 8));
assert(8, myArray3.length);

// toLocaleString
var n = 0;
var obj = { toLocaleString: function() { n++ } };
var myArray = [obj, obj, obj];
myArray.toLocaleString();
assert(3, n);

// set length
var array = [1, 2, 3];
array.length = 2;
assert(2, array.length);


//splice edge cases
myArray = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
myArray.splice(0, 0, -2, -1);
assert(12, myArray.length);