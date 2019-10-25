import i18n from '../i18n';

const LocaleService = {
    getRules(resource, options) {
        let rules = [];
        options = this.prepareOptions(options);
        
        this.addRequireRule(rules, resource, options);
        this.addPatternRule(rules, resource, options);
        this.addRangeRule(rules, resource, options);
        this.addLengthRule(rules, resource, options);
        this.addMaxLengthRule(rules, resource, options);
        this.addMinLengthRule(rules, resource, options);
        this.addValuesRule(rules, resource, options);
        this.addCompareRule(rules, resource, options);
        
        return rules;
    },

    prepareOptions(options){
        let getter = v => v;
        let compared = () => null;
        
        if (!options){
            options = { getter: getter, compared: compared };
        }
        if (!options.getter) options.getter = getter;
        if (!options.compared) options.compared = compared;
        
        return options;
    },
    
    addRequireRule(rules, resource, options){
        let settings = this.getRuleSettings(resource, 'Required');
        if(settings){
            rules.push(v => !!options.getter(v) || settings.message);
        }
    },

    addPatternRule(rules, resource, options){
        let settings = this.getRuleSettings(resource, 'Pattern');
        if(settings){
            rules.push(v => !!options.getter(v) || settings.message);
            rules.push(v => new RegExp(settings.value).test(options.getter(v)) || settings.message);
        }
    },

    addRangeRule(rules, resource, options){
        let settings = this.getRuleSettings(resource, 'Range');
        if(settings){
            let values = settings.value.split('-');
            rules.push(v => !!options.getter(v) || settings.message);
            rules.push(v => parseInt(options.getter(v)) >= values[0] && 
                parseInt(options.getter(v)) <= values[1] || settings.message);
        }
    },

    addLengthRule(rules, resource, options){
        let settings = this.getRuleSettings(resource, 'Length');
        if(settings){
            let values = settings.value.split('-');
            rules.push(v => !!options.getter(v) || settings.message);
            rules.push(v => options.getter(v).length >= values[0] && 
                options.getter(v).length <= values[1] || settings.message);
        }
    },

    addMaxLengthRule(rules, resource, options){
        let settings = this.getRuleSettings(resource, 'MaxLength');
        if(settings){
            rules.push(v => !!options.getter(v) || settings.message);
            rules.push(v => options.getter(v).length <= settings.value || settings.message);
        }
    },

    addMinLengthRule(rules, resource, options){
        let settings = this.getRuleSettings(resource, 'MinLength');
        if(settings){
            rules.push(v => !!options.getter(v) || settings.message);
            rules.push(v => options.getter(v).length >= settings.value || settings.message);
        }
    },

    addValuesRule(rules, resource, options){
        let settings = this.getRuleSettings(resource, 'Values');
        if(settings) {
            let values = settings.value.split(',');
            rules.push(v => !!options.getter(v) || settings.message);
            rules.push(v => !!values.find(x => x.trim() === options.getter(v)) ||
                settings.message);
        }
    },

    addCompareRule(rules, resource, options){
        let settings = this.getRuleSettings(resource, 'Compare');
        if(settings) {
            rules.push(() => {
                return settings.value === '' || !!settings.value || settings.message
            });
            rules.push(v => {
                return options.getter(v) === options.compared() || settings.message;
            });
        }
    },

    getRuleSettings(resource, rule){
        let value = this.getRuleValue(resource, rule);
        let message = this.getRuleMessage(resource, rule, value);
        return value === '' || value ? { value: value, message: message } : null;
    },
    
    getRuleValue(resource, rule){
        let key =`${resource}${rule}`;
        return this.getI18nValue(key);
    },

    getDisplayName(resource){
        let key =`${resource}DisplayName`;
        return this.getI18nValue(key);
    },

    getRuleMessage(resource, rule, value){
        let key =`${resource}${rule}Message`;
        return i18n.t(key, [this.getDisplayName(resource), value]);
    },
    
    getI18nValue(key){
        let value = i18n.t(key);
        return value !== key ? value : null;
    }
};

export { LocaleService }