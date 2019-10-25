import i18n from '../i18n';

const ErrorHandlerService = {
    parseResponseError (error) {
        if (error.toString().includes('Network Error')) {
            return i18n.t('Account.ServerUnavailable')
        }

        if (error.response) {
            let statusText = error.response.statusText;
            if (error.response.data && error.response.data.errors) {
                let message = '';
                for (let property in error.response.data.errors) {
                    error.response.data.errors[property].forEach(function (entry) {
                        if (entry) {
                            message += entry + '\n'
                        }
                    })
                }
                return message
            } else if (error.response.data && error.response.data.message) {
                return error.response.data.message
            } else if (statusText) {
                return statusText
            }
        }
    }
};

export { ErrorHandlerService }