<template>
  <div class="row justify-content-center">
    <div class="col-md-auto">
      <b-card title="Авторизация">
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
          <b-button type="submit" variant="primary">Авторизоваться</b-button>
        </b-form>
      </b-card>
    </div>
  </div>
</template>

<script src="./index.ts" lang="ts">
</script>

<style scoped src="./index.css">

</style>
