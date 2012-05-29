using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jint;
using Jint.Native;
using System.Reflection;
using System.IO;

namespace Jint.Temp {
    public class MyClass
    {
        public string Description { get; set; }
        public int Status { get; set; }

        public MyClass(string description, int status)
        {
            this.Description = description;
            this.Status = status;
        }
    }
    public class Base {
        public int square(int a, int b) {
            return a * b;
        }
    }

    class Program {
        
    
        
        static void Main(string[] args)
        {
            var jint = new JintEngine().DisableSecurity();
            var script = new StreamReader(@"scripts\test.js").ReadToEnd();

            jint.SetFunction("print", new Action<object>(Console.WriteLine));

            jint.Run(script);

            Console.ReadKey();
        }
    
        
        static void Main2(string[] args)
        {
            
            List<MyClass>myClasses = new List<MyClass>();
            myClasses.Add(new MyClass("Some text",2));
            myClasses.Add(new MyClass("More text", 1));

            var jintEngine = new JintEngine();
            jintEngine.SetParameter("myClasses", myClasses);

            Console.WriteLine("Result: {0}", jintEngine.Run("return myClasses[0].Description"));
            System.Console.ReadKey();
        }

        static void Main3(string[] args) {
            JintEngine engine = new JintEngine();
            engine.DisableSecurity();
            engine.Run("1;");
            Marshaller marshal = engine.Global.Marshaller;

            JsConstructor ctor = engine.Global.Marshaller.MarshalType(typeof(Baz));
            ((JsObject)engine.Global)["Baz"] = ctor;
            ((JsObject)engine.Global)["Int32"] = engine.Global.Marshaller.MarshalType(typeof(Int32));

            JsObject o = new JsObject();
            o["abc"] = new JsString("sure",engine.Global.StringClass.PrototypeProperty); 
            engine.SetParameter("o", o );
            engine.SetParameter("ts1", new TimeSpan(1000));
            engine.SetParameter("ts2", new TimeSpan(2000));

            engine.Run(@"
if (ts1 <= ts2) {
    System.Console.WriteLine('ts1 < ts2');
}
System.Console.WriteLine('{0}',o.abc);
System.Console.WriteLine('{0}',Jint.Temp.InfoType.Name);
");

            

            engine.Run(@"
System.Console.WriteLine('=========FEATURES==========');
var test = new Baz();
var val;
System.Console.WriteLine('test.Name: {0}', test.Name);
System.Console.WriteLine('test.CurrentValue: {0}', test.CurrentValue);

System.Console.WriteLine('Update object using method');
test.SetTimestamp(System.DateTime.Now);
System.Console.WriteLine('test.CurrentValue: {0}', test.CurrentValue);

System.Console.WriteLine('Update object using property');
test.CurrentValue = new System.DateTime(1980,1,1);
System.Console.WriteLine('test.CurrentValue: {0}', test.CurrentValue);

System.Console.WriteLine('Update object using field');
test.t = new System.DateTime(1980,1,2);
System.Console.WriteLine('test.CurrentValue: {0}', test.CurrentValue);


System.Console.WriteLine('Is instance of Baz: {0}', test instanceof Baz ? 'yes' : 'no' );
System.Console.WriteLine('Is instance of Object: {0}', test instanceof Object ? 'yes' : 'no' );
System.Console.WriteLine('Is instance of String: {0}', test instanceof String ? 'yes' : 'no' );

System.Console.WriteLine('Constant field Int32.MaxValue: {0}', Int32.MaxValue);

System.Console.WriteLine('========= INHERITANCE FROM A CLR TYPE ==========');
function Foo(name,desc) {
    Baz.call(this,name);

    this.Description = desc;
    this.SetTimestamp(System.DateTime.Now);
}

(function(){
    var func = new Function();
    func.prototype = Baz.prototype;
    Foo.prototype = new func();
    Foo.prototype.constructor = Foo;
})();

Foo.prototype.PrintInfo = function() {
    System.Console.WriteLine('{0}: {1} ({2})', this.Name,this.Description,this.t);
}

var foo = new Foo('Gib','Mega mann');
foo.PrintInfo();

System.Console.WriteLine('========= DUMP OBJECT ==========');

function ___StandAlone() {}

for (var prop in foo){
    try {
        val = foo[prop];
    } catch(err) {
        val = 'Exception: ' + err.toString();
    }
    System.Console.WriteLine('{0} = {1}',prop,val.toString());
}

System.Console.WriteLine('========= DUMP PROTOTYPE ==========');

foo = Foo.prototype;

for (var prop in foo){
    try {
        val = foo[prop];
    } catch(err) {
        val = 'Exception: ' + err.toString();
    }
    System.Console.WriteLine('{0} = {1}',prop,val.toString());
}

System.Console.WriteLine('========= DUMP OBJECT PROTOTYPE ==========');

foo = Object.prototype;

for (var prop in foo){
    try {
        val = foo[prop];
    } catch(err) {
        val = 'Exception: ' + err.toString();
    }
    System.Console.WriteLine('{0} = {1}',prop,val.toString());
}

System.Console.WriteLine('========= TYPE INFORMATION ==========');
//System.Console.WriteLine('[{1}] {0}', test.GetType().FullName, test.GetType().GUID);
for(var prop in Baz) {
    try {
        val = Baz[prop];
    } catch (err) {
        val = 'Exception: ' + err.toString();
    }

    System.Console.WriteLine('{0} = {1}',prop,val);
}

System.Console.WriteLine('========= PERFORMANCE ==========');
");
            int ticks = Environment.TickCount;
            engine.Run(@"
            var temp;
            for (var i = 0; i < 100000; i++)
                temp = new Baz('hi');
            ");

            Console.WriteLine("new objects: {0} ms", Environment.TickCount - ticks);
            
            ticks = Environment.TickCount;
            engine.Run(@"
            var temp = new Baz();
            var val = ToInt32(20);
            System.Console.WriteLine('Debug: {0} + {1} = {2}', '10', val, temp.Foo('10',val));
            for (var i = 0; i < 100000; i++)
                temp.Foo('10',val);
            ");

            Console.WriteLine("method call in {0} ms", Environment.TickCount - ticks);

            ticks = Environment.TickCount;
            engine.Run(@"
            var temp = new Baz();
            for (var i = 0; i < 100000; i++)
                temp.Foo();
            ");

            Console.WriteLine("method call without args {0} ms", Environment.TickCount - ticks);

            ticks = Environment.TickCount;
            engine.Run(@"
            var temp = new Baz();
            for (var i = 0; i < 100000; i++)
                temp.CurrentValue;
            ");

            Console.WriteLine("get property {0} ms", Environment.TickCount - ticks);

            ticks = Environment.TickCount;
            engine.Run(@"
            var temp = new Baz();
            for (var i = 0; i < 100000; i++)
                temp.t;
            ");

            Console.WriteLine("get field {0} ms", Environment.TickCount - ticks);

            ticks = Environment.TickCount;
            engine.Run(@"
            for (var i = 0; i < 100000; i++)
                /**/1;
            ");

            Console.WriteLine("empty loop {0} ms", Environment.TickCount - ticks);

            //JsInstance inst = ctor.Construct(new JsInstance[0], null, visitor);

            Console.ReadKey();
            
            return;
        }
    }

    public enum InfoType
    {
        Type = 1,
        Name = 2,
        Description = 3
    }

    public struct DummyStruct
    {
        public int x;
        public DateTime t;

        public string Name
        {
            get { return "Bill"; }
        }

        public void SetTimestamp(DateTime time)
        {
            t = time;
        }

        public DateTime CurrentValue
        {
            get { return t; }
            set { t = value; }
        }
    }

    public class Bar<T1>
    {
        public T2 Fn<T2,T3>(T1 a1, T2 a2)
        {
            return default(T2);
        }
    }

    public class Baz
    {
        string m_name;
        public DateTime t;
        public Baz(string name)
        {
            m_name = name;
        }
        public Baz()
        {
            m_name = "Bazz";
            t = new DateTime(1980, 1, 1);
        }

        public void SetTimestamp(DateTime date) {
            t = date;
        }

        public void SetName(string name)
        {
            m_name = name;
        }

        public DateTime CurrentValue
        {
            get { return t; }
            set { t = value; }
        }

        public string Name
        {
            get { return m_name; }
        }
        public static T Arrays<T>() where T: new() {
            return new T();
        }

        public static void UpdateObject<T>(T val)
        {

        }

        public static void ByRefArg(ref DummyStruct i, ref Baz o, out int res)
        {
            res = default(int);
            i.ToString();
            o.Foo();
            o.Zoo(i);
        }

        public void Foo()
        {
        }
        public virtual void Zoo(DummyStruct z)
        {
        }
        public int Foo(int a)
        {
            return a;
        }
        public int Foo(int a, int b)
        {
            return a + b;
        }
        public T Foo<T>(T a, T b)
        {
            return b;
        }
        public T Foo<T>(T a)
        {
            return a;
        }
        public T1 Foo<T1,T2>(T1 a, T2 b)
        {
            return a;
        }
    }
}
