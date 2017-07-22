﻿using SharpNeat.Domains.FunctionRegression;

namespace SharpNeat.Domains.GenerativeFunctionRegression
{
    public class GenerativeFnRegressionEvaluator : FnRegressionEvaluator
    {
        /// <summary>
        /// Construct a generative function regression evaluator with the provided parameter sampling info and function to regress.
        /// </summary>
        public GenerativeFnRegressionEvaluator(IFunction fn, ParamSamplingInfo paramSamplingInfo, double gradientMseWeighting)
            : base(fn, paramSamplingInfo, gradientMseWeighting, CreateGenerativeBlackBoxProbe(fn, paramSamplingInfo))
        {
        }

        private static GenerativeBlackBoxProbe CreateGenerativeBlackBoxProbe(IFunction fn, ParamSamplingInfo paramSamplingInfo)
        {
            // Determine the mid output value of the function (over the specified sample points) and a scaling factor
            // to apply the to neural network response for it to be able to recreate the function (because the neural net
            // output range is [0,1] when using the logistic function as the neuron activation function).
            FnRegressionUtils.CalcFunctionMidAndScale(fn, paramSamplingInfo, out double mid, out double scale);

            var blackBoxProbe = new GenerativeBlackBoxProbe(paramSamplingInfo, mid, scale);
            return blackBoxProbe;
        }
    }
}
