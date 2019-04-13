<template>
    <div class="login">
        <div class="row">
            <div class="col-12 main">
                <form v-if="loginForm" @submit.prevent="loginUser">
                    <input type="email" v-model="creds.email" placeholder=" Email" class="shadow">
                    <input type="password" v-model="creds.password" placeholder=" Password" class="ml-2 shadow">
                    <button type="submit" class="btn btn-sm btn-dark ml-2 shadow">Login</button>
                </form>
                <form v-else @submit.prevent="register">
                    <input type="text" v-model="newUser.username" placeholder=" Name" class="shadow">
                    <input type="email" v-model="newUser.email" placeholder=" Email" class="ml-2 shadow">
                    <input type="password" v-model="newUser.password" placeholder=" Password" class="ml-2 shadow">
                    <button type="submit" class="btn btn-sm btn-dark ml-2 shadow">Create Account</button>
                </form>
                <div @click="loginForm = !loginForm" class="mt-3">
                    <p v-if="loginForm" class="reg text-light">No account? Click to Register.</p>
                    <p v-else class="text-light reg">Already have an account click to Login</p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

    export default {
        name: "login",
        mounted() {
            //checks for valid session
            this.$store.dispatch("authenticate");
        },
        data() {
            return {
                loginForm: true,
                creds: {
                    email: "",
                    password: ""
                },
                newUser: {
                    email: "",
                    password: "",
                    username: ""
                }
            };
        },
        methods: {
            register() {
                this.$store.dispatch("register", this.newUser);
            },
            loginUser() {
                this.$store.dispatch("login", this.creds);
            }
        }
    };
</script>

<style>
    .login {
        min-height: 100vh;
        background-image: linear-gradient(to right, #ff7300 30%, #ffc400);
    }

    .main {
        margin-top: 30vh;
    }

    .reg {
        cursor: pointer;
    }
</style>