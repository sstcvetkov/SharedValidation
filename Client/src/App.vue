<!--suppress SpellCheckingInspection -->
<template>
  <v-app>
    <v-container fluid fill-height >
      <v-layout align-center justify-center >
        <v-flex xs12 sm8 md4 >
          <v-content>
            <v-card class="elevation-12">
              <v-card-text>
                <v-form v-model="isValid" ref="form" lazy-validation>
                  <v-text-field :label="displayFor('Name')" :rules="rulesFor('Name')" v-model="model.Name" 
                                type="text" prepend-icon="mdi-account"></v-text-field>
                  <v-text-field :label="displayFor('Email')" :rules="rulesFor('Email')" v-model="model.Email" 
                                type="text" prepend-icon="mdi-email"></v-text-field>
                  <v-text-field :label="displayFor('Password')" :rules="rulesFor('Password')" v-model="model.Password"
                                type="password" prepend-icon="mdi-lock"></v-text-field>
                  <v-text-field :label="displayFor('PasswordConfirm')" 
                                :rules="rulesFor('PasswordConfirm', { compared:() => model.Password })"
                                type="password" prepend-icon="mdi-lock"></v-text-field>
                  <v-text-field :label="displayFor('Age')" :rules="rulesFor('Age')" v-model="model.Age"
                                type="number" prepend-icon="mdi-meditation"></v-text-field>
                  <v-select :label="displayFor('Language')" :rules="rulesFor('Language')" :items="cultures"
                            @change="onCultureChange" prepend-icon="mdi-web"></v-select>
                </v-form>
              </v-card-text>
              <v-card-actions >
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="onSubmit">Войти</v-btn>
              </v-card-actions>
            </v-card>
          </v-content>
        </v-flex>
      </v-layout>
    </v-container>
    <template v-if="alert">
      <v-snackbar :timeout="20000" multi-line :color="alert.color" 
                  @input="alert = null" :top="true" :value="true" >
        <span>{{ alert.message }}</span>
        <v-btn text dark @click.native="alert = null">Close</v-btn>
      </v-snackbar>
    </template>
  </v-app>
</template>

<script>
import { LocaleService } from './services/locale.service';
import { ErrorHandlerService } from './services/errorHandler.service';
const LocaleSpace = 'Controllers-AccountController.';

export default {
  name: 'App',
  data () {
    return {
      alert: null,
      cultures: [
        { flag: 'us', value: 'en-US', locale: 'en', text: 'English' },
        { flag: 'ru', value: 'ru-RU', locale: 'ru', text: 'Русский' }
      ],
      model: {
        Name: '',
        Email: '',
        Password: '',
        Age: '',
        Culture: ''
      },
      isValid: false
    }
  },
  methods: {
    onSubmit () {
      if (this.$refs.form.validate()) {
        this.$http.post('http://localhost:5000/Account/Registration', this.model)
            .then(response => {
              this.alert = { message: response.data, color: 'success' };
            })
            .catch(error => {
              let message = ErrorHandlerService.parseResponseError(error);
              this.alert = { message: message, color: 'error' };
            })
      }
    },
    rulesFor(propertyName, options){
      return LocaleService.getRules(`${LocaleSpace}${propertyName}`, options)
    },
    displayFor(propertyName){
      return LocaleService.getDisplayName(`${LocaleSpace}${propertyName}`)
    },
    onCultureChange (value) {
      let culture = this.cultures.find(x => x.value === value);
      this.model.Culture = culture.value;
      if (culture.locale !== this.$i18n.locale) {
        this.$i18n.locale = culture.locale;
        this.$refs.form.resetValidation();
      }
    }
  }
};
</script>
