﻿using System;
using Its.Validation;

namespace Its.Validation
{
    internal class FailureMessageTemplate : MessageTemplate
    {
        private readonly Func<FailedEvaluation, string> buildMessage;

        protected FailureMessageTemplate()
        {
        }

        public FailureMessageTemplate(string value) : this(_ => value)
        {
        }

        public FailureMessageTemplate(Func<FailedEvaluation, string> buildMessage)
        {
            if (buildMessage == null)
            {
                throw new ArgumentNullException("buildMessage");
            }
            this.buildMessage = buildMessage;
        }

        public override string GetMessage(RuleEvaluation evaluation)
        {
            var failedEvaluation = evaluation as FailedEvaluation ?? new FailedEvaluation();
            var template = buildMessage(failedEvaluation);
            if (evaluation == null)
            {
                return template;
            }
            return MessageGenerator.Detokenize(template, failedEvaluation.Parameters);
        }

        public override string ToString()
        {
            return GetMessage(null);
        }
    }
}