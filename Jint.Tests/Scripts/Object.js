// 15.2.2

var a = new Object();
alert(a.toString());

assert(true, Object.hasOwnProperty.call({ foo: 3 }, 'foo'));
assert(false, Object.hasOwnProperty.call({ foo: 3 }, 'dummy'));

function foo() {

}

(function($foo) { $foo.bar = function() { }; })(foo)

assert('function', typeof foo.bar);

function A () {
};

A.prototype.fld = "a";

function B () {
};

B.prototype = new A();
B.prototype.constructor = B;

var inst = new B();
var instA = new A();

assert("a",inst.fld);

A.prototype.fld = "b";

assert("b",inst.fld);

assert("b",instA.fld);

assert("b",B.prototype.fld);

B.prototype.fld = "c";

assert("c",inst.fld);

assert("b",A.prototype.fld);
assert("b",instA.fld);

instA.fld = "x";

assert("b",A.prototype.fld);
assert("x",instA.fld);
assert("c",inst.fld);