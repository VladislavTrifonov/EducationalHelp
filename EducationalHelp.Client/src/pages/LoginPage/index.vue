<template>
  <div class="row justify-content-center">
    <div class="col-md-6">
      <b-alert v-if="redirected" show variant="info">
        Вы были перенаправлены сюда с другой страницы, требующей авторизации. <br>
        После авторизации Вы будете автоматически перенаправлены обратно.
      </b-alert>
      <b-card title="Авторизация" v-if="isLogin">
        <b-form @submit="onSubmit">
          <b-form-group
              id="input-group-1"
              label="Логин:"
              label-for="input-1"
              description="В это поле необходимо ввести логин (латинские буквы), а не псевдоним."
          >
            <b-form-input
                id="input-1"
                v-model="loginForm.login"
                type="text"
                required
            ></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-2" label="Пароль:" label-for="input-2">
            <b-form-input
                id="input-2"
                type="password"
                v-model="loginForm.password"
                required
            ></b-form-input>
          </b-form-group>
          <div class="row justify-content-center">
            <div class="col-md-auto">
              <b-button type="submit" variant="primary" class="mr-2">Авторизоваться</b-button>
              <b-button variant="outline-primary" @click="isLogin = false">У меня еще нет аккаунта...</b-button>
            </div>
          </div>
        </b-form>
      </b-card>

      <b-card title="Регистрация" v-else>
        <b-form @submit="onRegisterSubmit">
          <b-form-group
              id="input-group-11"
              label="Логин:"
              label-for="input-11"
              description="В это поле необходимо ввести логин (латинские буквы)."
          >
            <b-form-input
                id="input-11"
                v-model="registerForm.login"
                type="text"
                required
            ></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-22"
                        label="Псевдоним:"
                        label-for="input-22"
                        description="В это поле можете ввести желаемое обращение к своей личности."
          >
            <b-form-input
                id="input-22"
                type="text"
                v-model="registerForm.pseudonym"
                required
            ></b-form-input>
          </b-form-group>
            <b-form-group id="input-group-222" label="Пароль:" label-for="input-222">
              <b-form-input
                  id="input-222"
                  type="password"
                  v-model="registerForm.password"
                  required
              ></b-form-input>
            </b-form-group>
            <b-form-group id="input-group-2222"
                          label="Повторите пароль:"
                          label-for="input-2222"
                          description="Пароль и его повторение должны совпадать"
            >
              <b-form-input
                  id="input-2222"
                  type="password"
                  v-model="registerForm.passwordRepeat"
                  required
              ></b-form-input>
            </b-form-group>
          <div class="row justify-content-center">
            <div class="col-md-auto">
              <b-button type="submit" variant="primary" class="mr-2">Зарегистрироваться</b-button>
              <b-button variant="outline-primary" @click="isLogin = true">У меня уже есть аккаунт...</b-button>
            </div>
          </div>
        </b-form>
      </b-card>

    </div>
  </div>
</template>

<script src="./index.ts" lang="ts">
</script>

<style scoped src="./index.css">

</style>
