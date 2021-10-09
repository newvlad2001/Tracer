using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Tracer.Library.Logic;
using Tracer.Library.Logic.Impl;

namespace Tracer.UnitTests
{
    [TestFixture]
    public class Tests
    {
        private ITracer _tracer;

        [SetUp]
        public void Setup()
        {
            _tracer = new TracerImpl();
        }

        public void TestMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        public void TracerImpl_ThreadCountingIsCorrect(int amOfThreads)
        {
            var threads = new List<Thread>();
            for (int i = 0; i < amOfThreads; i++)
            {
                threads.Add(new Thread(TestMethod));
            }

            foreach (var t in threads)
            {
                t.Start();
                t.Join();
            }

            Assert.AreEqual(amOfThreads, _tracer.GetTraceResult().ThreadTraces.Count);
        }

        [TestCase(0)]
        [TestCase(100)]
        [TestCase(200)]
        public void TracerImpl_ThreadExecutionTimeIsGreaterOrEqualThanTimeout(int timeout)
        {
            _tracer.StartTrace();
            Thread.Sleep(timeout);
            _tracer.StopTrace();
            
            long execTime = _tracer.GetTraceResult().ThreadTraces[Thread.CurrentThread.ManagedThreadId].ThreadTime;
            Assert.IsTrue(execTime >= timeout);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void TracerImpl_ThreadMethodsCountingIsCorrect(int amOfMethods)
        {
            for (int i = 0; i < amOfMethods; i++)
            {
                TestMethod();
            }

            int result = _tracer.GetTraceResult().ThreadTraces[Thread.CurrentThread.ManagedThreadId].MethodsInfo.Count;
            Assert.AreEqual(amOfMethods, result);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        public void TracerImpl_ThreadInnerMethodsCountingIsCorrect(int amOfInners)
        {
            _tracer.StartTrace();
            for (int i = 0; i < amOfInners; i++)
            {
                TestMethod();
            }
            _tracer.StopTrace();

            _tracer.GetTraceResult().ThreadTraces.TryGetValue(Thread.CurrentThread.ManagedThreadId, out var threadTrace);
            int result = _tracer.GetTraceResult().ThreadTraces[Thread.CurrentThread.ManagedThreadId].MethodsInfo[0].InnerMethods.Count;
            Assert.AreEqual(amOfInners, result);
        }
    }
}